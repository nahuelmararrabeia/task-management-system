import { AuthRepository } from "@/app/features/auth/repositories/interfaces/AuthRepository";

export class RefreshTokenUseCase {
  constructor(private repo: AuthRepository) {}

  async execute() {
    await this.repo.refreshToken();
  }
}