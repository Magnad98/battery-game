const onScanSuccess = (qrCodeMessage) => {
    document.getElementById('input').hidden = true
    document.getElementById('output').hidden = false
    document.getElementById('result-title').innerText = 'Success!'
    document.getElementById('result-title').classList.add('result-success')
    document.getElementById('result-text').innerText = 'Thank you for recycling! Your QR Code was scanned succesfully. A bonus level should be loaded into the game in short time. Stay green!'

    fetch('/pusher', {
        method: 'POST',
        headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
        },
        body: qrCodeMessage
    })
}

const onScanError = (errorMessage) => {
    document.getElementById('input').hidden = true
    document.getElementById('output').hidden = false
    document.getElementById('result-title').innerText = 'Error'
    document.getElementById('result-title').classList.add('result-error')
    document.getElementById('result-text').innerText = 'Sorry, but an error occured! Please scan your code again.'
}

const html5QrcodeScanner = new Html5QrcodeScanner('reader', { fps: 10, qrbox: 250 })

html5QrcodeScanner.render(onScanSuccess, onScanError)

document.getElementById('rescan-button').addEventListener('click', function() {
    location.reload()
}, false)