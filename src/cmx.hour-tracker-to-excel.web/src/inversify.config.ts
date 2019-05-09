import { Container } from "inversify";
import "reflect-metadata";
import { AuthenticationService } from "./services/AuthenticationService";
import { TYPES } from "./types";

const container = new Container();

container
  .bind<AuthenticationService>(TYPES.AuthenticationService)
  .to(AuthenticationService);

export { container };
