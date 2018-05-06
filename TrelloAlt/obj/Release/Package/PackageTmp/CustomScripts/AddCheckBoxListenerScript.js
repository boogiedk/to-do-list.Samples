$(document).ready(function () {

    $('.ActiveCheck').change(function () {

        var self = $(this);
        var id = self.attr('id');
        var value = self.prop('checked');

        $.ajax({
            url: '/ToDoActModels/AjaxEdit',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            sucess: function(result) {
                $('#tableDivElement').html(result);
            }
        });
    });
});