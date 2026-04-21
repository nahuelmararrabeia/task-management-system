import { useState } from "react";
import { Task } from "../../entities/Task";
import { useDI } from "@/app/providers";

export function useTask() {
  const { getTask } = useDI().tasks;

  const [selectedTask, setSelectedTask] = useState<Task | null>(null);
  const [loading, setLoading] = useState(false);

  async function loadTask(id: string) {
    setLoading(true);
    try {
      const task = await getTask.execute(id);
      setSelectedTask(task);
    } finally {
      setLoading(false);
    }
  }

  function clearTask() {
    setSelectedTask(null);
  }

  return {
    selectedTask,
    loadingTask: loading,
    loadTask,
    clearTask,
  };
}