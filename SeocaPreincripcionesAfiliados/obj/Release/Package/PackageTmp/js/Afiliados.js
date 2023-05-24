$(".btnHistorial").click(function (eve) {
    $("#modal-content").load("/Home/verDatoAfiliado/" + $(this).data("id")); // GET
});

function cerrarId01() {
    document.getElementById('id01').style.display = 'none';
}