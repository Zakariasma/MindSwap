export class User {
  id: number;
  username: string;
  email: string;
  passwordHash: string;
  passwordSalt: string;
  creationDate: Date;

  constructor(id: number, username: string, email: string, passwordHash: string, passwordSalt: string, creationDate: Date) {
      this.id = id;
      this.username = username;
      this.email = email;
      this.passwordHash = passwordHash;
      this.passwordSalt = passwordSalt;
      this.creationDate = creationDate;
  }
}
