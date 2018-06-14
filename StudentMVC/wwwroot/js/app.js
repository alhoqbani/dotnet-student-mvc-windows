(function ($) {
    'use strict';

    $(document).ready(function () {

        $('#delete_from_show_page').on('click', function (e) {
            e.preventDefault();

            $(this).prop("disabled", true);
            $(this).children().removeClass('d-none');

            if (!confirm("Are You Sure?")) {
                $(this).prop("disabled", false);
                $(this).children().addClass('d-none');

                return;
            }

            var el = this;
            $.ajax({
                url: '/students/' + $(this).data('delete-id'),
                method: 'DELETE',
            })
                .then(() => window.location.replace("/students"))
                .catch(err => {
                    console.log(err);
                    $(el).prop("disabled", false);
                    $(el).children().addClass('d-none');
                    alert('Sorry, something went wrong!!');
                });
        });

        // Delete a student from the list in index page
        $('.delete_from_list').on('click', function (e) {
            e.preventDefault();

            if (!confirm("Are You Sure?")) {
                return;
            }

            $(this).prop("disabled", true);
            $(this).children().removeClass('d-none');
            var el = this;

            $.ajax({
                url: '/students/' + $(this).data('delete-id'),
                method: 'DELETE',

                success: function (data, textStatus, jqXHR) {
                    $(el).closest('tr').fadeOut();
                },

                error: function (err) {
                    console.log(err);
                    $(el).prop("disabled", false);
                    $(el).children().addClass('d-none');
                    alert('Sorry, something went wrong!!');
                }

            });
        });




    });

})(jQuery)