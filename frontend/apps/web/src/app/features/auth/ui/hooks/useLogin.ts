import { useDI } from "@/app/providers";
import { useState } from "react";
import { LoginParams } from "./login.types";
import { useRouter } from "next/navigation";

export function useLogin() {
  const router = useRouter();
  const { login } = useDI().auth;
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function tryLogin({ email, password }: LoginParams) {
    setLoading(true);
    setError(null);

    try {
      await login.execute(email, password);
      router.push("/features/tasks");
    } catch {
      setError("Invalid credentials");
    } finally {
      setLoading(false);
    }
  }

  return {
    tryLogin,
    loading,
    error
  };
}