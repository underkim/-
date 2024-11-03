package dailydiary.back.diaryservice.controller.user;

import dailydiary.back.diaryservice.domain.user.User;
import dailydiary.back.diaryservice.service.user.UserService ;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.Optional;

@RestController
@RequestMapping("/api")  // 기본 URL 경로
public class UserController {

    @Autowired
    private UserService userService;

    // JWT 비밀 키와 만료 시간 설정
    private final String SECRET_KEY = "your_secret_key"; // 비밀 키
    private final long EXPIRATION_TIME = 864_000_000; // 10일 (밀리초)

    // 사용자 등록
//    @PostMapping("/register")
//    public ResponseEntity<String> registerUser(@RequestBody User user) {
//        userService.registerUser(user.getUsername(), user.getPassword());
//        return new ResponseEntity<>("User registered successfully", HttpStatus.CREATED);
//    }
    @PostMapping("/register")
    public ResponseEntity<String> registerUser(@RequestBody User user) {
        userService.registerUser(user.getUsername(), user.getPassword());
        return new ResponseEntity<>("User registered successfully", HttpStatus.CREATED);
    }

    // 사용자 로그인
    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody User user) {
        Optional<User> authenticatedUser = userService.authenticate(user.getUsername(), user.getPassword());

        if (authenticatedUser.isPresent()) {
            String token = generateToken(authenticatedUser.get().getUsername());
            return ResponseEntity.ok(new LoginResponse(token, "로그인 성공"));
        }

        return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("로그인 실패"));
    }

    // JWT 토큰 생성 메서드
    private String generateToken(String username) {
        return Jwts.builder()
                .setSubject(username)
                .setIssuedAt(new Date(System.currentTimeMillis()))
                .setExpiration(new Date(System.currentTimeMillis() + EXPIRATION_TIME))
                .signWith(SignatureAlgorithm.HS256, SECRET_KEY)
                .compact();
    }

    // 로그인 응답 클래스
    private static class LoginResponse {
        private String token;
        private String message;

        public LoginResponse(String token, String message) {
            this.token = token;
            this.message = message;
        }

        public String getToken() {
            return token;
        }

        public String getMessage() {
            return message;
        }
    }

    // 오류 응답 클래스
    private static class ErrorResponse {
        private String message;

        public ErrorResponse(String message) {
            this.message = message;
        }

        public String getMessage() {
            return message;
        }
    }
}
