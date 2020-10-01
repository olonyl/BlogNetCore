var dataTable;
$(document).ready(function () {
    loadDatatable();
});

function loadDatatable() {
    dataTable = $("#tblCategory").DataTable({
        "ajax": {
            "url": "/admin/categories/getAll",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "50%" },
            { "data": "sort", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/admin/categories/edit/${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px;">
                                    <i class="fas fa-edit"></i>Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete('/admin/categories/delete/${data}')  class="btn btn-danger text-white" style="cursor: pointer; width: 100px;">
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
        title: "Do you want to delete this record?",
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