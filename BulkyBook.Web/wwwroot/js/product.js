var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#table').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            {
                "data": "title", className: 'dt-body-left', "width": "30%"
            },
            {
                "data": "isbn", className: 'dt-body-left'
            },
            {
                "data": "price", className: 'dt-body-left'
            },
            {
                "data": "author", className: 'd	978-0142424179	6.1	John Green	Romancet-body-left'
            },
            {
                "data": "category.name", className: 'dt-body-left'
            },
            {
                "data": "id", "render": function (data) {
                    return `
                            <div class="d-grid gap-2 d-md-block">
                                <a class="btn btn-outline-dark shadow-none" role="button" href="/Admin/Product/Upsert?id=${data}"><i class="bi bi-pencil"></i></a>
                                <a class="btn btn-outline-dark shadow-none" role="button" onClick=Delete('/Admin/Product/Delete/${data}')><i class="bi bi-trash3"></i></a>
                            </div>
                           `
                },
                className: 'dt-body-left'
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}