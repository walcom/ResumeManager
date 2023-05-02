// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('#loaderbody').addClass('hide');

    $.notify.defaults({ globalPosition: 'top left' });

    $(document).bind('ajaxStart', function () {
        $('#loaderbody').removeClass('hide');
    }).bind('ajaxStop', function () {
        $('#loaderbody').addClass('hide');
    });
});

$(".custom-file-input").on("change", function () {
    var filename = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(filename);
});


function DeleteItem(btn) {
    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2) {
        //alert('This row cannot be deleted!');
        return;
    }

    $(btn).closest('tr').remove();

    CalcTotalExperiences();
}

function CalcTotalExperiences() {
    var x = document.getElementsByClassName('YearsWorked');
    var totalExp = 0;
    var i;

    for (i = 0; i < x.length; i++) {
        totalExp = totalExp + eval(x[i].value);
    }

    document.getElementById('TotalExperience').value = totalExp;
    return;
}

document.addEventListener('change', function (e) {
    if (e.target.classList.contains('YearsWorked')) {
        CalcTotalExperiences();
    }
}, false);


function AddItem(btn) {
    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');

    var rowOuterHtml = rows[rows.length - 1].outerHTML;

    var lastrowIdx = rows.length - 2; //document.getElementById('hdnLastIndex').value; //
    var nextrowIdx = eval(lastrowIdx) + 1;
    //document.getElementById('hdnLastIndex').value = nextrowIdx;

    rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);

    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;

    //var btnAddID = btn.id;
    //var btnDeleteid = btnAddID.replaceAll('btnadd', 'btnremove');

    //var delbtn = document.getElementById(btnDeleteid);
    //delbtn.classList.add('visible');
    //delbtn.classList.remove('invisible');

    //var addbtn = document.getElementById(btnAddID);
    //addbtn.classList.remove('visible');
    //addbtn.classList.add('invisible');

    var x = document.getElementsByTagName("INPUT");

    for (var cnt = 0; cnt < x.length; cnt++) {
        if (x[cnt].type == "text" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
            x[cnt].value = '';
        else if (x[cnt].type == "number" && x[cnt].id.indexOf('_' + nextrowIdx + '_') > 0)
            x[cnt].value = 0;
    }

    rebindValidators();
}


function rebindValidators() {
    var $form = $('applicantForm');
    $form.unbind();
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form);
    $form.validate($form.data("unobtrusivevalidation").options);
}


showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}


JqueryAjaxPost = form => {
    try {
        //console.log(form);

        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {                
                if (res.isValid) {
                    $('#view-all').html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');

                    $("#form-modal").modal('hide');

                    $.notify('Submitted Successfully', "success");
                    // , { globalPosition: 'top left'}
                }
                else
                    $("#form-modal .modal-body").html(res.html);
            },
            error: function (err) {
                console.log(err);
            }
        })

        // to prevent default form submit
        return false;
    }
    catch (ex) {
        console.log(ex);
    }
};


jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record? ')) {
        try {
            //console.log(form);

            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    //$.notify('Deleted Successfully', { globalPosition: 'top center', class:'success' });
                },
                error: function (err) {
                    console.log(err);
                }
            })
        }
        catch (ex) {
            console.log(ex);
        }
    }

    // to prevent default form submit
    return false;
};