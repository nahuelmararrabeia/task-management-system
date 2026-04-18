import { AuthRepository } from "@/app/features/auth/repositories/interfaces/AuthRepository";

export class LoginUseCase {
  constructor(private repo: AuthRepository) {}

  async execute(email: string, password: string) {
    await this.repo.login(email, password);
  }
}