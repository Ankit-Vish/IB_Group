

function fileUpload () {
    // add jQuery ajax code to upload
    debugger;
    var files = $('#fileUpload').prop("files");
    var url = "OnPostMyUploader";
    formData = new FormData();
    formData.append("MyUploader", files[0]);
    jQuery.ajax({
        type: 'POST',
        url: url,
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (repo) {
            if (repo.status == "success") {
                alert("File : " + repo.fileName + " is uploaded successfully");
                $("#fileName").val(repo.fileName);
                $("#newfileName").val(repo.filePath);
            }
        },
        error: function () {
            alert("Error occurs");
        }
    });
};

