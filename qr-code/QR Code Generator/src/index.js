const generateQRCode = () => {
  let website = document.getElementById("website").value;
  if (website) {
    let qrcodeContainer = document.getElementById("qrcode");
    qrcodeContainer.innerHTML = "";
    new QRCode(qrcodeContainer, {
      text: website,
      width: 256,
      height: 256,
      colorDark: "#000000",
      colorLight: "#ffffff",
      correctLevel: QRCode.CorrectLevel.H
    });
    document.getElementById("qrcode-container").style.display = "block";
  } else {
    alert("Please enter a valid URL");
  }
}