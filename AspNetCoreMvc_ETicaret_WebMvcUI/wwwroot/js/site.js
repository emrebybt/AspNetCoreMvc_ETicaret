function AddToCart(id) {
    $.ajax({
        type: "POST",
        url: "/Cart/AddCart",
        data: { id: id, quantity: 1 },
        success: function (response) {
            Swal.fire({
                position: 'bottom-end',
                icon: 'success',
                title: 'Ürün Sepete Eklendi',
                showConfirmButton: false,
                timer: 1500
            }),
                $("#cartdropdown").load("Cart/DropdownRefresh")
            $("#cartcount").load("Cart/CartCount")
        }
    })
}




