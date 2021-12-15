$(document).ready(function () {
    $("#keyword").keyup(function () {
        var strkeyword = $('#keyword').val();
        $.ajax({
            url: '/Admin/Search/FindProduct',
            /*dataType: "json",*/
            type: "GET",
            data: { keyword: strkeyword },
            async: true,
            success: function (results) {
                $("#records_table").html("");
                $("#records_table").html(results);
            },
            error: function (xhr) {
                alert('error');
            }
        });
    });
});