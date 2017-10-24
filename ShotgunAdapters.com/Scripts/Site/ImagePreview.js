/* ****************************** Image Upload Preview ***************************************** */
function imageUploadPreview(targetId) {
    var e = window.event;
    for (var i = 0; i < e.srcElement.files.length; i++) {
        var file = e.srcElement.files[i];
        var $targetImg = $(targetId);
        var reader = new FileReader();
        reader.onloadend = function () {
            $targetImg.attr("src", reader.result);
        };
        reader.readAsDataURL(file);
    }
}