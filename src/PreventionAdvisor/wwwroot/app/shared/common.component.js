"use strict";
var CommonComponent = (function () {
    function CommonComponent() {
    }
    CommonComponent.prototype.getAPILocation = function () {
        if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
            return "http://localhost:5000/";
        }
        return "";
    };
    return CommonComponent;
}());
exports.CommonComponent = CommonComponent;
//# sourceMappingURL=common.component.js.map