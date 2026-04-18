"use client";

import { createContext, useContext } from "react";
import { createTasksContainer } from "@/app/features/tasks/task.container";
import { createAuthContainer } from "@/app/features/auth/auth.container";
import React from "react";

type DIContainer = {
  auth: ReturnType<typeof createAuthContainer>;
  tasks: ReturnType<typeof createTasksContainer>;
};

const DIContext = createContext<DIContainer | null>(null);

export function Providers({ children }: { children: React.ReactNode }) {
  const container = React.useMemo<DIContainer>(() => ({
    auth: createAuthContainer(),
    tasks: createTasksContainer(),
  }), []);
  
  return (
    <DIContext.Provider value={container}>
      {children}
    </DIContext.Provider>
  );
}

export function useDI() {
  const ctx = useContext(DIContext);

  if (!ctx) {
    throw new Error("useDI must be used within Providers");
  }

  return ctx;
}