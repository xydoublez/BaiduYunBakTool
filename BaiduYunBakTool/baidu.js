function lzqUpload(file) {
    setTimeout(function () {
        //调用C#方法上传文件
        window.external.UploadBaidu(file);
    }, 2000);
    //打开文件
    $("#h5Input0")[0].click();
   

}