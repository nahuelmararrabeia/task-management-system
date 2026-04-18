import { LoginUseCase } from "./use-cases/login";
import { RefreshTokenUseCase } from "./use-cases/refreshToken";
import { AuthApiRepository } from "./repositories/AuthApiRepository";

export function createAuthContainer() {
  const authRepository = new AuthApiRepository();

  return {
    login: new LoginUseCase(authRepository),
    refreshToken: new RefreshTokenUseCase(authRepository)
  };
}