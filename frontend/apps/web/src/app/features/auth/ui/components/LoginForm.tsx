"use client";

import { useState } from "react";
import { Form, FormControl, FormError, FormField, FormLabel, Input, Button } from "@task-management/ui";

interface LoginFormProps {
  onSubmit: (data: { email: string; password: string }) => void;
  loading?: boolean;
  error?: string | null;
}

export function LoginForm({ onSubmit, loading, error }: LoginFormProps) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    onSubmit({ email, password });
  }

  return (
    <Form onSubmit={handleSubmit} className="flex flex-col gap-4">
      <FormField>
        <FormLabel>Email</FormLabel>
        <FormControl>
          <Input value={email} onChange={(e) => setEmail(e.target.value)} />
        </FormControl>
      </FormField>

      <FormField>
        <FormLabel>Password</FormLabel>
        <FormControl>
          <Input value={password} onChange={(e) => setPassword(e.target.value)} />
        </FormControl>
      </FormField>

      <FormError>{error}</FormError>

      <Button type="submit" loading={loading}>
        Login
      </Button>
    </Form>
  );
}