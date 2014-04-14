//create friendly urlslug
//$.fn.slug = function () {
//    return this.val().replace(/[^a-zA-Z 0-9-]+/g, '').toLowerCase().replace(/\s/g, '-');
//};

function convertToSlug(text) {
    return text
        .toLowerCase()
        .replace(/[^\w ]+/g, '')
        .replace(/ +/g, '-')
    ;
}

function initPager() {
    $('#recordPerPage').change(function () {
        var pageSize = $(this).val();
        var targetUrl = $('#pagingUrl').val();
        window.location = targetUrl + "&pageSize=" + pageSize;
    });
}


function initWysiwyg()
{
    //Fix bundleconfig
    tinyMCE.baseURL = '/scripts/tinymce',
    tinymce.init({
        selector: "textarea.editme",
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime media table contextmenu paste"
        ],
        //theme_advanced_buttons3_add: "fullscreen",
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image media"
    });
}

function initTitleSlug() {
    $(".slugMe").keyup(function () {
        debugger;
        var slugged = convertToSlug(this.value);
        $(".slugHere").val(slugged);
    });
}


$(document).ready(function () {
    $(function () {
        $(".validation-summary-errors").addClass('alert alert-danger');
        $(".validation-summary-errors").prepend('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>');
    });

    if ($('.editme').length) {
        initWysiwyg();
    }

    if ($('.slugMe').length) {
        initTitleSlug();
    }

    if ($('#recordPerPage').length) {
        initPager();
    }
});