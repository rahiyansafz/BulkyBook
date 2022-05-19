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
                                <a class="btn btn-outline-dark shadow-none" role="button" ><i class="bi bi-trash3"></i></a>
                            </div>
                           `
                },
                className: 'dt-body-left'
            }
        ]
    });
}
