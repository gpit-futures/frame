import axios from "axios";

export class UserService {
  constructor() {
    this.frameworkBackendUrl = "http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080"
  }

  private userList: IUser[];
  private frameworkBackendUrl : string;

  async getUsers(): Promise<IUser[]> {
    this.userList = await axios.get(`${this.frameworkBackendUrl}/api/user-lists`)
      .then(response => response.data);
    return this.userList;
  }

  async getUser(id: string): Promise<IUser | undefined> {
    await this.getUsers();
    return this.userList.find(x => x.id === id);
  }
}

export interface IUser {
  id: string;
  username: string;
  title: string;
  firstName: string;
  lastName: string;
}
