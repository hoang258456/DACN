$(document).ready(function () {
    $(function () {
        $(".add-to-cart").click(function () {
            var productid = $('#MaSp').val();
            var soLuong = $('#txtsoluong').val();
            $.ajax({
                url: '/api/cart/add',
                type: "POST",
                dataType: "JSON",
                data: {
                    SanphamID: productid,
                    Soluong: soLuong
                },
                success: function (response) {
                    if (response.resuilt == 'Redirect') {
                        window.location = response.url;
                    } else {
                        loadHeaderCart();
                        location.reload();
                        //loadHeaderCart();
                        //window.location = 'cart.html';
                    }
                   
                },
                error: function (error) {
                    alert("the was an error posting the data to the server" + error.responseText);
                }
            });
        });
        function loadHeaderCart() {
            $("#miniCart").load("/AjaxContent/HeaderCart");
            $("#numberCart").load("/AjaxContent/NumberCart");
        }
        //$(".add-to-cart").click(function () {
        //    var productid = $('#MaSp').val();
        //    var soLuong = $('#txtsoluong').val();
        //    $.ajax({
        //        url: '/api/cart/add',
        //        type: "POST",
        //        dataType: "JSON",
        //        data: {
        //            SanphamID: productid,
        //            Soluong: soLuong
        //        },
        //        success: function (response) {
        //            if (response.resuilt == 'Redirect') {
        //                window.location = response.url;
        //            } else {
        //                loadHeaderCart();
        //                location.reload();
           
        //            }
        //            //if (resuilt.success) {
        //            //    loadHeaderCart();
        //            //    location.reload();
        //            //} else {
        //            //    window.location = response.url;
        //            //}

        //        },
        //        error: function (error) {
        //            alert("the was an error posting the data to the server" + error.responseText);
        //        }
        //    });
        //});
        $(".removecart").click(function () {
            var productid = $(this).attr("data-mahh");
            $.ajax({
                url: "api/cart/remove",
                type: "POST",
                dataType: "JSON",
                data: { SanphamID: productid },
                success: function (resuilt) {
                    if (resuilt.success) {
                        loadHeaderCart();
                        window.location = 'cart.html';
                    }
                },
                error: function (rs) {
                    alert("Remove Cart Error ! ");
                }
            });
        });
        $(".cartItem").click(function () {
            var productid = $(this).attr("data-mahh");
            var soLuong = parseInt($(this).val());
            $.ajax({
                url: "api/cart/update",
                type: "POST",
                dateType: "JSON",
                data: {
                    SanphamID: productid,
                    Soluong: soLuong
                },
                success: function (resuilt) {
                    if (resuilt.success) {
                        loadHeaderCart();
                        window.location = 'cart.html';
                    }
                },
                error: function (rs) {
                    alert("Cập Nhật Giỏ Hàng Thất Bại");
                }
            });
        });
    });
})
