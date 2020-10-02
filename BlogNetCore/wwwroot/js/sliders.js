var dataTable;
$(document).ready(function () {
    loadDatatable();
});

function loadDatatable() {
    dataTable = $("#tblSlider").DataTable({
        "ajax": {
            "url": "/admin/sliders/getAll",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "55%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/admin/sliders/edit/${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px;">
                                    <i class="fas fa-edit"></i>Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete('/admin/sliders/delete/${data}')  class="btn btn-danger text-white" style="cursor: pointer; width: 100px;">
                                    <i class="fas fa-trash-alt"></i>Delete
                                </a>
                            </div>
                        `;
                },
                "width":"30%"
            }
        ],
        "language": {
            "emptyTable":"No Records to be displayed"
        },
        "width":"100%"
        });
}

function Delete(url) {
    swal({
        title: "Do you want to delete this slider?",
        text: "You can't undo this action",
        buttons: ["Cancel!", "Yes, delete it"],
        dangerMode: true,
        closeModal: true,
    }).then((value) => {
        if(value)
        $.ajax({
            type: "DELETE",
            url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                } else {
                    toastr.error(data.message);
                }

            }
        });
    });
}