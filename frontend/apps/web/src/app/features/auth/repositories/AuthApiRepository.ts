import { AuthRepository } from "@/app/features/auth/repositories/interfaces/AuthRepository";
import { apiClient } from "@/app/shared/http/clients";

export class AuthApiRepository implements AuthRepository {
  async login(email: string, password: string): Promise<void> {
    const response = await apiClient.post("/Auth/login", {
      email,
      password
    });

    if (!response.ok) {
      throw new Error("Invalid credentials");
    }
  }

  async refreshToken(): Promise<void> {
    await apiClient.post("/Auth/refresh-token", {});
  }
}