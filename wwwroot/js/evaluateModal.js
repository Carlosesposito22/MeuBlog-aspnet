document.addEventListener('DOMContentLoaded', function () {

    const placeholder = document.getElementById('modal-placeholder');

    // Usamos 'event delegation' no 'body' para capturar cliques no botão,
    // o que é mais eficiente e funciona mesmo que o botão seja adicionado dinamicamente.
    document.body.addEventListener('click', function (e) {

        // Verifica se o elemento clicado tem a nossa classe-alvo
        if (e.target && e.target.classList.contains('js-open-evaluate-modal')) {
            const blogId = e.target.dataset.blogId;
            const url = `/Blog/EvaluateBlog/${blogId}`;

            // Busca o conteúdo do modal no servidor
            fetch(url)
                .then(response => response.text())
                .then(modalHtml => {
                    // Coloca o HTML do modal no placeholder
                    placeholder.innerHTML = modalHtml;

                    // Exibe o modal usando a API do Bootstrap
                    const modalElement = document.getElementById('evaluateModal');
                    const bootstrapModal = new bootstrap.Modal(modalElement);
                    bootstrapModal.show();

                    // Anexa o listener de submit AO FORMULÁRIO QUE ACABOU DE SER CARREGADO
                    attachFormSubmitListener(bootstrapModal);
                })
                .catch(error => console.error("Erro ao carregar o modal: ", error));
        }
    });

    function attachFormSubmitListener(modalInstance) {
        const form = document.getElementById('formAvaliacao');
        if (!form) return;

        form.addEventListener('submit', function (event) {
            event.preventDefault();

            const formData = new FormData(form);

            fetch(form.action, {
                method: 'POST',
                body: formData
            })
            .then(response => {
                modalInstance.hide(); // Fecha o modal em caso de sucesso ou erro
                if (response.ok) {
                    window.location.reload();
                }
            })
            .catch(error => console.error("Erro ao enviar o formulário: ", error));
        });
    }
});