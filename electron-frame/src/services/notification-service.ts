import axios from "axios";

export class NotificationService {
  constructor() { }

  private notificationList: INotifications[];

  async getNotifications(token:string): Promise<INotifications[]> {
    if (!this.notificationList) {
      this.notificationList = await axios.get('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/notifications', 
      {headers: {
        "Authorization" : 'Bearer ' + token
      }
    })
      .then(response => response.data);
    }
    console.log(this.notificationList);
    return this.notificationList;
  }

  async removeNotification(token:string, id: string) {
    await axios.post('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/notifications/'+ id +'/read', null,
      {headers: {
        "Authorization" : 'Bearer ' + token
      }
    })
      .then(response => response.data);
    }
}

export interface INotifications {
  id: string;
  ods: string;
  read: string[];
  summary: string;
  details: string;
  type: string;
  dateCreated: string;
}
