document.addEventListener("DOMContentLoaded", function () {
    const senha = document.getElementById("pwd-cliente");
    const confirmarSenha = document.getElementById("pwd-confirm-cliente");

    confirmarSenha.addEventListener("input", function () {
        if (senha.value !== confirmarSenha.value) {
            confirmarSenha.setCustomValidity("Senhas diferentes");
        } else {
            confirmarSenha.setCustomValidity("");
        }
    });
});