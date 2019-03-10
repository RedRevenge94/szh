
export class RoutingComponent{

  public static getCutlivationDetailsLink(id: number) {
    return 'cultivation/' + id;
  }

  public static getPlantTunnelDetailsLink(plantId){
    return 'plants/' + plantId;
  }

  public static getTunnelDetailsLink(tunnelId){
    return 'tunnels/' + tunnelId;
  }

}
