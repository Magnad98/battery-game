const generateQRCode = () => {
    let recycledBatteries = {
      "NineVolt": document.getElementById("NineVolt-quantity").value,
      "D": document.getElementById("D-quantity").value,
      "C": document.getElementById("C-quantity").value,
      "AA": document.getElementById("AA-quantity").value,
      "AAA": document.getElementById("AAA-quantity").value,
      "Cell": document.getElementById("Cell-quantity").value,
    }
    const nullFormCheck = (obj) => obj.NineVolt > 0 || obj.D > 0 || obj.C > 0 || obj.AA > 0 || obj.AAA > 0 || obj.Cell > 0
    if (nullFormCheck(recycledBatteries)) {
      let qrcodeContainer = document.getElementById("qrcode");
      qrcodeContainer.innerHTML = "";
      new QRCode(qrcodeContainer, {
        text: JSON.stringify(recycledBatteries),
        width: 256,
        height: 256,
        colorDark: "#000000",
        colorLight: "#ffffff",
        correctLevel: QRCode.CorrectLevel.H
      });
      document.getElementById("qrcode-container").style.display = "block";
      console.log(recycledBatteries)
    } else {
      alert("You need to recycle at least 1 battery to obtain a QR Code!");
    }
}