"use client";

import { useState } from "react";
import { Button, Input } from "@task-management/ui";

export function CreateTaskButton() {
  const [open, setOpen] = useState(false);
  const [title, setTitle] = useState("");

  const handleCreate = () => {
    console.log("Create task:", title);
    setTitle("");
    setOpen(false);
  };

  if (!open) {
    return (
      <Button variant="primary" onClick={() => setOpen(true)}>
        + Nueva tarea
      </Button>
    );
  }

  return (
    <div className="flex gap-2 items-center">
      <Input
        autoFocus
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Título de la tarea..."
        onKeyDown={(e) => {
          if (e.key === "Enter") handleCreate();
        }}
      />

      <Button
        variant="primary"
        onClick={handleCreate}
        disabled={!title.trim()}
      >
        Crear
      </Button>
    </div>
  );
}