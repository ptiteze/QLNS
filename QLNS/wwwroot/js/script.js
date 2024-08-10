


$(document).ready(function () {
    $(".aa-add-card-btn").click(function (event) {
        event.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>

        var productId = $(this).data("productid");
        var quantity = $(this).data("quantity");

        console.log("Product ID:", productId);
        console.log("Quantity:", quantity);

        if (!productId) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Product ID is null",
                footer: '<a href="#">Reload lại trang?</a>'
            });
            return;
        }

        $.ajax({
            url: '/Cart/AddToCart', // Thay thế bằng URL thực tế của bạn
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                productid: productId,
                quantity: quantity
            }),
            success: function (data) {
                console.log(data);
                if (data.error) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: data.message,
                        footer: '<a href="#">Reload lại trang?</a>'
                    });
                } else {
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Thêm vào giỏ hàng thành công",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $("#length_order").html(data.length_order);
                }
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Thêm vào giỏ hàng thất bại",
                    footer: '<a href="#">Reload lại trang?</a>'
                });
            }
        });
    });
    $("#aa-add-to-card-btn").click(function (event) {
        event.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>

        var productId = $(this).data("productid");
        var quantity = $(this).data("quantity");

        console.log("Product ID:", productId);
        console.log("Quantity:", quantity);

        if (!productId) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Product ID is null",
                footer: '<a href="#">Reload lại trang?</a>'
            });
            return;
        }

        $.ajax({
            url: '/Cart/AddToCart', // Thay thế bằng URL thực tế của bạn
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                productid: productId,
                quantity: quantity
            }),
            success: function (data) {
                console.log(data);
                if (data.error) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: data.message,
                        footer: '<a href="#">Reload lại trang?</a>'
                    });
                } else {
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Thêm vào giỏ hàng thành công",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $("#length_order").html(data.length_order);
                }
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Thêm vào giỏ hàng thất bại",
                    footer: '<a href="#">Reload lại trang?</a>'
                });
            }
        });
    });
});