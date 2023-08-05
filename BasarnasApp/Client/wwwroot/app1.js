document.addEventListener("DOMContentLoaded", function () {
    Notification.requestPermission((x) => {

    })

    window.OnKejadianFire = (message) => {
        var sound = new Audio();
        sound.src = 'alarm.mp3';
        sound.muted = false;
        sound.play();

        new Notification("Success, Anda Dapat Menerima Notifikasi !")
    }

    window.initMap = async () => {
        const { Map } = await google.maps.importLibrary("maps");

        map = new Map(document.getElementById("map1"), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 10,
        });
    }

    window.mapKejadian = async (latlng) => {
        const { Map } = await google.maps.importLibrary("maps");
        var data = latlng.split(',');

        mylocation = { lat: eval(data[0]), lng: eval(data[1]) };

        map = new Map(document.getElementById("map1"), {
            center: mylocation,
            zoom: 10,
        });

        new google.maps.Marker({
            position: mylocation,
            map,
            title: "Lokasi Kejadian",
        });


    }

}, false);


