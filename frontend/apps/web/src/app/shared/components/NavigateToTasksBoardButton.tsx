"use client";

import { Button } from "@task-management/ui";
import { useRouter } from "next/navigation";

export function NavigateToTasksBoardButton() {
  const router = useRouter();

  const handleClick = () => {
    router.push("/tasks");
  };

  return (
    <Button variant="primary" onClick={handleClick}>
      Go to Tasks Board
    </Button>
  );
}