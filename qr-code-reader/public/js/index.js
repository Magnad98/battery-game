onScanSuccess = (qrCodeMessage) => {
    document.getElementById('result').innerHTML = '<span class="result">QR Code scanned succesfully!</span>';
    console.log(JSON.parse(qrCodeMessage))
}

onScanError = (errorMessage) => {
    //handle scan error
}

let html5QrcodeScanner = new Html5QrcodeScanner("reader", { fps: 10, qrbox: 250 });

html5QrcodeScanner.render(onScanSuccess, onScanError);