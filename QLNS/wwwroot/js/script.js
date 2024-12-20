


$(document).ready(function () {
    //$(".aa-add-card-btn").click(function (event) {
    //    event.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>

    //    var productId = $(this).data("productid");
    //    var quantity = $(this).data("quantity");

    //    console.log("Product ID:", productId);
    //    console.log("Quantity:", quantity);

    //    if (!productId) {
    //        Swal.fire({
    //            icon: "error",
    //            title: response.error,
    //            text: "Product ID is null",
    //            footer: '<a href="#">Reload lại trang?</a>'
    //        });
    //        return;
    //    }

    //    $.ajax({
    //        url: '/Cart/AddToCart', // Thay thế bằng URL thực tế của bạn
    //        type: 'POST',
    //        contentType: 'application/json',
    //        data: JSON.stringify({
    //            productid: productId,
    //            quantity: quantity
    //        }),
    //        success: function (data) {
    //            console.log(data);
    //            if (data.error) {
    //                Swal.fire({
    //                    icon: "error",
    //                    title: response.error,
    //                    text: data.message,
    //                    footer: '<a href="#">Reload lại trang?</a>'
    //                });
    //            } else {
    //                Swal.fire({
    //                    position: "center",
    //                    icon: "success",
    //                    title: "Thêm vào giỏ hàng thành công",
    //                    showConfirmButton: false,
    //                    timer: 1500
    //                });
    //                $("#length_order").html(data.length_order);
    //            }
    //        },
    //        error: function () {
    //            Swal.fire({
    //                icon: "error",
    //                title: response.error,
    //                text: "Thêm vào giỏ hàng thất bại",
    //                footer: '<a href="#">Reload lại trang?</a>'
    //            });
    //        }
    //    });
    //});
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
                text: "Chọn sản phẩm không phù hợp",
                footer: '<a href="#">Reload lại trang?</a>'
            });
            return;
        }

        $.ajax({
            url: '/Cart/AddCart', // Thay thế bằng URL thực tế của bạn
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
                        title: response.error,
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
$(document).ready(function () {
    //$("#buyForm").submit(function (event) {
    //    event.preventDefault(); // Ngăn hành động submit mặc định

    //    $.ajax({
    //        type: "POST",
    //        url: '@Url.Action("Buy", "Order")',
    //        data: $(this).serialize(),
    //        success: function (response) {
    //            if (response.error) {
    //                Swal.fire({
    //                    position: "center",
    //                    icon: "error",
    //                    title: response.error,
    //                    showConfirmButton: true
    //                });
    //            } else if (response.orderId) {
    //                Swal.fire({
    //                    position: "center",
    //                    icon: "success",
    //                    title: "Mua hàng thành công",
    //                    showConfirmButton: false,
    //                    timer: 1500
    //                });

    //                setTimeout(function () {
    //                    window.location.href = '/';
    //                }, 1500);
    //            }
    //        },
    //        error: function () {
    //            Swal.fire({
    //                position: "center",
    //                icon: "error",
    //                title: "Có lỗi xảy ra",
    //                showConfirmButton: true
    //            });
    //        }
    //    });
    //});
    $("form[name='formRegister']").submit(function (event) {
        event.preventDefault(); // Ngăn hành động submit mặc định

        $.ajax({
            type: "POST",
            url: '@Url.Action("RegisterResult", "Account")',
            data: $(this).serialize(),
            success: function (response) {
                if (response.error) {
                    Swal.fire({
                        position: "center",
                        icon: "error",
                        title: response.error,
                        showConfirmButton: true
                    });
                } else if (response.check) {
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Tạo tài khoản thành công",
                        showConfirmButton: false,
                        timer: 1500
                    });

                    setTimeout(function () {
                        window.location.href = '/Home/Login';
                    }, 1500);
                }
            },
            error: function () {
                Swal.fire({
                    position: "center",
                    icon: "error",
                    title: "Có lỗi xảy ra",
                    showConfirmButton: true
                });
            }
        });
    });
    
});
$(document).ready(function () {
    // Xử lý khi click vào ngôi sao
    $(".star").on("click", function () {
        var ratingValue = $(this).data("value");

        // Xóa hết các class active trước đó
        $(".star i").removeClass("active");

        // Thêm class active cho các sao đã chọn
        $(this).nextAll().find("i").addClass("active");
        $(this).find("i").addClass("active");

        // Gửi giá trị rating đến server hoặc xử lý
        console.log("Rating value: " + ratingValue);
        // Thêm code gửi ratingValue về server tại đây (ví dụ qua Ajax)
    });
});