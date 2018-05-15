export namespace Users {
  export interface IUser {
    id: string;
    username: string;
    title: string;
    firstName: string;
    lastName: string;
  }

  const userList : IUser[] = [
    {
      id: "366f6345-3407-4f6c-9c77-108a37543064",
      username: "john.smith1",
      title: "Dr",
      firstName: "John",
      lastName: "Smith"
    },
    {
      id: "d3ea56ce-87d2-4a9a-afb9-e8d820299bf1",
      username: "joe.bloggs5",
      title: "Mr",
      firstName: "Joe",
      lastName: "Bloggs"
    }
  ];

  export async function getUsers(): Promise<IUser[]> {
    return userList;
  }

  export async function getUser(id: string): Promise<IUser> {
    return userList.find(x => x.id === id);
  }
}
