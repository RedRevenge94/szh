import { environment } from "../../environments/environment";

export class ApiUrlConfigurator{

    static GetApiUrl(){
        var globalUrl = window.location.href;
        var splitted = globalUrl.split("//", 2); 
        var spliited2 = splitted[1].split("/",1);
        var serverAddress = spliited2[0].split(":",2);
        var ip = serverAddress[0];
        var port = serverAddress[1];
        return "http://" + ip + ":" + environment.API_Port + environment.API_Context;
        //return "http://" + "dev.zbiksoft.pl" + ":" + environment.API_Port + environment.API_Context;
    }

}