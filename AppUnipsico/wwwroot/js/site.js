// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function uploadFile() {
    var formData = new FormData();
    var fileInput = document.getElementById('fileInput');
    var file = fileInput.files[0];
    formData.append('file', file);

    fetch('Controllers/DatasController/InserirDatasDisponiveis', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error('Erro ao enviar arquivo');
        })
        .then(data => {
            console.log(data);
        })
        .catch(error => {
            console.error('Erro:', error);
        });
}
