import { useEffect, useState } from "react";
import { Task } from "../../entities/Task";
import { useDI } from "@/app/providers";

export function useTasks() {
  const { getTasks } = useDI().tasks;
  const [data, setData] = useState<Task[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getTasks.execute().then((result) => {
      setData(result);
      setLoading(false);
    });
  }, [getTasks]);

  return { tasks: data, loadingGetTasks: loading };
}