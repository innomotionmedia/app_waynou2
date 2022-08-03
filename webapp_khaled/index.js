var fullApiCall

GetURLParams();

async function GetURLParams() {

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    const adId = urlParams.get('adId')
    const languageCode = urlParams.get('local')

    if(adId == '' || adId == null)
    {
        alert('Sorry, but something went wrong :-(')
        return
    }

    LoadAd(adId, languageCode)
}

async function LoadAd(adId, languageCode){

    var dict = { tblAdID: adId };
    var x = await callKumulosAPIFunction(null, dict, 'tblAds_getFullAdInfo')
    console.log(x)

    document.getElementById("mainpicture").src = "data:image/png;base64,"+x.payload[0].fullPic; 

    switch(languageCode) {
        case 'de':
            document.getElementById("label_description").innerText = x.payload[0].descriptionGER; 
            document.getElementById("label_title").innerText = x.payload[0].titleDe; 
          break;
        case 'ar':
            document.getElementById("label_description").innerText = x.payload[0].descriptionAR; 
            document.getElementById("label_title").innerText = x.payload[0].titleAr; 
          break;
        default:
            document.getElementById("label_title").innerText = x.payload[0].title; 
            document.getElementById("label_description").innerText = x.payload[0].descriptionENG; 
      } 

      document.getElementById("label_addresse").innerText = x.payload[0].adresse; 
      document.getElementById("label_hours").innerText = x.payload[0].hours; 
      document.getElementById("label_telephone").innerText = x.payload[0].telephone; 
      document.getElementById("label_email").innerText = x.payload[0].email; 
      document.getElementById("label_web").innerText = x.payload[0].web; 

      fullApiCall = x;
}

// coming from html
function OpenBrowserWithAdresse(){
    window.open("http://maps.google.com/?q=" + fullApiCall.payload[0].adresse); 
}

function OpenWebPage(){
    window.open(fullApiCall.payload[0].web); 
}

function OpenTelephone(){
    var telephone = fullApiCall.payload[0].telephone
    window.location.assign("tel:"+telephone, "_self");
}



function OpenEmailService(){
    var emailTo = fullApiCall.payload[0].email
    var emailCC = ''
    var emailSub = ''
    var emailBody = ''
    window.location.assign('mailto:'+emailTo+'?cc='+emailCC+'&subject='+emailSub+'&body='+emailBody, '_self');
}




async function callKumulosAPIFunction(event, dict, functionName) {

    //doenst work without, would reload and then fail for some reason...
    if (event != null) {
        event = event || window.event;
        event.preventDefault();
    }


    let url = "https://api.kumulos.com/b2.2/91b17c3a-2be0-451f-83a4-fe6d8a19347e/" + functionName + ".json"; // old url
    let username = '91b17c3a-2be0-451f-83a4-fe6d8a19347e';
    let password = '8HRDrrjrOSTEgEfZnQgPxpYf2Q3ZyM799v/9';

    let headers = new Headers();

    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    headers.append('Authorization', 'Basic ' + username + ":" + password);


    let body = Object.entries(dict).map(([key, value]) =>
        encodeURIComponent(`params[${key}]`) + '=' + encodeURIComponent(value)).join('&');

    var res

    await fetch(url, {
        method: 'POST',
        headers: {
            Authorization: 'Basic ' + btoa(username + ":" + password),
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: body
    }).then(response => response.json())
        .then(data => {
            //console.log(data);
            res = data; // cannot return here. must await or something idk
        });


    return res;

}