"use client";

import { LoginForm } from "../components/LoginForm";
import { useLogin } from "../hooks/useLogin";

export function LoginView() {
  const { tryLogin, loading, error } = useLogin();

  return (
    <div className="min-h-screen flex items-center justify-center">
      <LoginForm
        onSubmit={tryLogin}
        loading={loading}
        error={error}
      />
    </div>
  );
}