import axios from "axios";

export class UserService {
  constructor() { }

  private userList: IUser[];

  async getUsers(): Promise<IUser[]> {
    this.userList = await axios.get('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/user-lists')
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
