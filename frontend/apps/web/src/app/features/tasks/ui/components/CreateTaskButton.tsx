"use client";

import { useState } from "react";
import { Button, Input } from "@task-management/ui";

export function CreateTaskButton({ onConfirmCreate }: { onConfirmCreate: (title: string) => void }) {
  const [open, setOpen] = useState(false);
  const [title, setTitle] = useState("");


  async function handleCreate() {
    if (!title.trim()) return;

    await onConfirmCreate(title);

    setTitle("");
    setOpen(false);
  }

  if (!open) {
    return (
      <div className="p-4 flex justify-end mx-4">
        <Button variant="primary" className="w-full" onClick={() => setOpen(true)}>
          + Nueva tarea
        </Button>
      </div>
    );
  }

  return (
    <div className="flex gap-2 items-center justify-end mx-4">
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