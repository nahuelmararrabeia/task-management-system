import { TaskApiRepository } from "./repositories/TaskApiRepository";
import { GetTasksUseCase } from "./use-cases/getTasks";
import { UpdateTaskStatusUseCase } from "./use-cases/updateTaskStatus";

export function createTasksContainer() {
  const taskRepository = new TaskApiRepository();

  return {
    taskRepository,
    getTasks: new GetTasksUseCase(taskRepository),
    changeTaskStatus: new UpdateTaskStatusUseCase(taskRepository),
  };
}