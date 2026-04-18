(function () {
    function getSignalRHubUrl() {
        return window.tvSignalRConfig?.getSignalRHubUrl?.() || "";
    }

    function getDeviceNo() {
        return window.tvSignalRConfig?.getDeviceNo?.() || "";
    }

    function updateSignalRGate(message, isError) {
        const gate = document.getElementById("signalrGate");
        const card = document.getElementById("signalrGateCard");
        if (!gate || !card) {
            return;
        }

        if (message) {
            card.textContent = message;
        }

        if (isError) {
            card.style.borderColor = "#ff5a5a";
            card.style.color = "#ffd4d4";
        }
    }

    function hideSignalRGate() {
        const gate = document.getElementById("signalrGate");
        if (gate) {
            gate.style.display = "none";
        }
        document.body.classList.remove("signalr-pending");
    }

    function showSignalRGate() {
        const gate = document.getElementById("signalrGate");
        if (gate) {
            gate.style.display = "grid";
        }
    }

    function buildHubUrl(hubUrl, deviceNo) {
        if (!hubUrl || !deviceNo) {
            return "";
        }
        const separator = hubUrl.includes("?") ? "&" : "?";
        return `${hubUrl}${separator}DeviceNo=${encodeURIComponent(deviceNo)}`;
    }

    function ensureConnected() {
        const hubUrl = getSignalRHubUrl();
        const deviceNo = getDeviceNo();
        if (!hubUrl || !deviceNo || !window.signalR) {
            return Promise.reject(new Error("SignalR configuration missing."));
        }

        const appState = window.tvSignalR || (window.tvSignalR = {});
        if (appState.connection && appState.connection.state === signalR.HubConnectionState.Connected) {
            return Promise.resolve(appState.connection);
        }
        if (appState.connectPromise) {
            return appState.connectPromise;
        }

        const connection = new signalR.HubConnectionBuilder()
            .withUrl(buildHubUrl(hubUrl, deviceNo), {
                withCredentials: false,
                transport: signalR.HttpTransportType.WebSockets,
                skipNegotiation: true
            })
            .withAutomaticReconnect()
            .build();

        connection.onclose(() => {
            updateSignalRGate("Failed to Connect to the Server", true);
            showSignalRGate();
        });

        connection.off("ReceiveMessage");
        connection.on("ReceiveMessage", (message) => {
            const eventKey = encodeURIComponent(message?.eventKey || "");
            window.location.href = `/Home/HandleSignal?eventKey=${eventKey}`;
        });

        appState.connection = connection;
        appState.connectPromise = connection.start()
            .then(() => connection)
            .catch((error) => {
                appState.connection = null;
                throw error;
            })
            .finally(() => {
                appState.connectPromise = null;
            });

        return appState.connectPromise;
    }

    window.tvSignalR = window.tvSignalR || {};
    window.tvSignalR.ensureConnected = ensureConnected;

    let connectFlow = Promise.resolve(); // Temporary for bypass purpose
    connectFlow = ensureConnected(); // Temporary for bypass purpose: comment this single line while SignalR server is down.
    //ensureConnected()
    connectFlow// remove connectFlow and uncmomment ensureConnected() 
        .then(() => {
            const path = window.location.pathname.toLowerCase();
            if (path === "/" || path === "/home" || path === "/home/index") {
                window.location.replace("/Home/Dashboard");
                return;
            }
            hideSignalRGate();
        })
        .catch(() => {
            updateSignalRGate("Failed to Connect to the Server", true);
        });
})();