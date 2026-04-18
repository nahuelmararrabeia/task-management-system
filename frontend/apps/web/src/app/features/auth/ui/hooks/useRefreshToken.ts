import { useDI } from "@/app/providers";
import { useRouter } from "next/navigation";

export function useRefreshToken() {
  const { refreshToken } = useDI().auth;
  const router = useRouter();

  return async function tryRefreshToken() {

    try {
      await refreshToken.execute();
    } catch {
      router.push("/auth");
    }
  }
}