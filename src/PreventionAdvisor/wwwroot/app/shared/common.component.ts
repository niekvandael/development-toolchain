export class CommonComponent {

    getAPILocation() {
        if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
           return "http://localhost:5000";
        }
        return "";
    }

}