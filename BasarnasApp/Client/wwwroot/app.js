document.addEventListener("DOMContentLoaded", function () {

    window.cetak = () => {
        window.print();
    }

    Notification.requestPermission((x) => {

    })

    window.OnKejadianFire = (message) => {
        if (Notification.permission === "granted") {
            createNotification(message);
        } else if (Notification.permission !== "granted") {
            Notification.requestPermission().then((permission) => {
                // If the user accepts, let's create a notification
                if (permission === "granted") {
                    createNotification(message);
                }
            });
        }
    }

    window.createNotification = (message) => {
        var notification = new Notification("Perhatian !!!!!", {
            body: message,
            icon: '/basarnas.png',
            data: {
                url: "/instansi/kejadian",
                status: "open",
            },
        });

        notification.onclick = (event) => {
            event.preventDefault(); // prevent the browser from focusing the Notification's tab
        };

        var au = document.createElement("audio");
        au.src = 'alarm.mp3';
        au.play();
        //var x = document.getElementById("myAudio");
        //x.play();
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
        latlng = latlng.split(",").join(".");
        var data = latlng.split(';');
        mylocation = { lat: parseFloat(data[0].trim()), lng: parseFloat(data[1].trim()) };

        map = new Map(document.getElementById("map1"), {
            center: mylocation,
            zoom: 15,
        });

        new google.maps.Marker({
            position: mylocation,
            map,
            title: "Lokasi Kejadian",
        });


    }

    window.soundPlayer = {
        audio: null,
        muted: false,
        playing: false,
        _ppromis: null,
        puse: function () {
            this.audio.pause();
        },
        play: function (file) {
            if (this.muted) {
                return false;
            }
            if (!this.audio && this.playing === false) {
                this.audio = new Audio(file);
                this._ppromis = this.audio.play();
                this.playing = true;

                if (this._ppromis !== undefined) {
                    this._ppromis.then(function () {
                        soundPlayer.playing = false;
                    });
                }

            } else if (!this.playing) {

                this.playing = true;
                this.audio.src = file;
                this._ppromis = soundPlayer.audio.play();
                this._ppromis.then(function () {
                    soundPlayer.playing = false;
                });
            }
        }
    };

}, false);


