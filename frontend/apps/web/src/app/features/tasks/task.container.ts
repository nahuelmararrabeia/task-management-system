import { TaskApiRepository } from "./repositories/TaskApiRepository";
import { CreateTaskUseCase } from "./use-cases/createTask";
import { GetTasksUseCase } from "./use-cases/getTasks";
import { UpdateTaskStatusUseCase } from "./use-cases/updateTaskStatus";

export function createTasksContainer() {
  const taskRepository = new TaskApiRepository();

  return {
    taskRepository,
    getTasks: new GetTasksUseCase(taskRepository),
    createTask: new CreateTaskUseCase(taskRepository),
    changeTaskStatus: new UpdateTaskStatusUseCase(taskRepository),
  };
}