$('.btn-download-app').click(function()
{
    DownloadApp();
});

$('.btn-get-help').click(function()
{
    GetHelp();
});


function DownloadApp(){
    window.location.replace(window.location.protocol + "//" + window.location.host + "/Download");

    alert('This Application Requires .NET 6.0');
}

function GetHelp(){
    window.location.replace(window.location.protocol + "//" + window.location.host + "/Help");
}