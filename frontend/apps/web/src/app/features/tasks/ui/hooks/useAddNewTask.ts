import { useDI } from "@/app/providers";
import { KanbanColumnData } from "@task-management/ui";
import { useState } from "react";

export function useAddNewTask() {
    const { createTask } = useDI().tasks;    
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    async function tryCreateTask(
        title: string, 
        setColumns: React.Dispatch<React.SetStateAction<KanbanColumnData[]>>
    ): Promise<any> {
        setLoading(true);
        setError(null);
        
        try {
            var newTaskId = await createTask.execute(title);
            setColumns((prev) => {
                const newColumns = structuredClone(prev);
            
                const pendingColumn = newColumns.find(c => c.id === "Pending");
            
                if (!pendingColumn) return prev;
            
                pendingColumn.cards.unshift({
                    id: newTaskId,
                    content: title,
                });
            
                return newColumns;
            });
        } catch {
            setError("Failed to create task");
        } finally {
            setLoading(false);
        }
    }


    return { tryCreateTask, loadingCreateTask: loading, error };

}